# Звіт з аналізу інкапсуляції в Open-Source проєкті

**Студент:** Котяш Тарас, група КН-3/1

## 1. Обраний проєкт

- **Назва:** .NET Runtime (dotnet/runtime)
- **Посилання на GitHub:** https://github.com/dotnet/runtime
- **Проаналізовані файли:**
  - `List<T>`: https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/List.cs
  - `Stack<T>` і вкладений `Stack<T>.Enumerator`: https://github.com/dotnet/runtime/blob/main/src/libraries/System.Collections/src/System/Collections/Generic/Stack.cs

## 2. Аналіз інкапсуляції

### Клас: `List<T>`

- **Опис класу:** динамічний список, який зберігає елементи у внутрішньому масиві й автоматично збільшує його розмір.
- **Поля:** дані зберігаються у внутрішніх полях, які не є частиною відкритого інтерфейсу. Зовнішній код працює зі списком лише через методи й властивості.

```csharp
private const int DefaultCapacity = 4;
internal T[] _items;
internal int _size;
internal int _version;
```

- **Властивості:** `Count` це властивість тільки для читання, вона повертає значення поля `_size`. Змінити кількість елементів напряму не можна, лише через `Add`, `Remove` та інші методи. `Capacity` має і `get`, і `set`, причому `set` містить перевірку.

```csharp
public int Count => _size;

public int Capacity
{
    get => _items.Length;
    set
    {
        if (value < _size)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
        }
        ...
    }
}
```

- **Індексатор:** `this[int index]` дозволяє працювати зі списком як з масивом (`list[0]`). Він перевіряє межі й при виході за них викидає виняток.

```csharp
public T this[int index]
{
    get
    {
        if ((uint)index >= (uint)_size)
        {
            ThrowHelper.ThrowArgumentOutOfRange_IndexMustBeLessException();
        }
        return _items[index];
    }
    set
    {
        if ((uint)index >= (uint)_size)
        {
            ThrowHelper.ThrowArgumentOutOfRange_IndexMustBeLessException();
        }
        _items[index] = value;
        _version++;
    }
}
```

- **Оператори:** у `List<T>` перевантажених операторів я не знайшов, вся робота йде через методи й індексатор.

### Клас: `Stack<T>`

- **Опис класу:** стек (останній зайшов, перший вийшов), реалізований на внутрішньому масиві.
- **Поля:** усі дані приховані в приватних полях, зовні доступні тільки методи й властивості.

```csharp
private T[] _array;
private int _size;
private int _version;
private const int DefaultCapacity = 4;
```

- **Властивості:** `Count` і `Capacity` це властивості тільки для читання, вони повертають значення з приватних полів. Змінити їх напряму не можна.

```csharp
public int Count => _size;
public int Capacity => _array.Length;
```

- **Конструктори:** є три: порожній, із заданою місткістю (з перевіркою, що вона не від'ємна) і з наповненням із колекції (з перевіркою на null).

```csharp
public Stack(int capacity)
{
    ArgumentOutOfRangeException.ThrowIfNegative(capacity);
    _array = new T[capacity];
}
```

- **Індексатори/Оператори:** у `Stack<T>` їх немає. Доступ до елементів іде тільки через `Push`, `Pop` і `Peek`, тобто лише до верхнього елемента.

### Клас: `Stack<T>.Enumerator`

- **Опис класу:** вкладена структура для перебору елементів стека.
- **Поля:** усі приватні, а частина з них `readonly`, тому після створення їх не можна змінити.

```csharp
private readonly Stack<T> _stack;
private readonly int _version;
private int _index;
private T? _currentElement;
```

- **Властивості:** `Current` тільки для читання, повертає `_currentElement`.
- **Захист від змін:** перебір запам'ятовує версію стека. Якщо стек змінили під час перебору, `MoveNext` викидає `InvalidOperationException`.

## 3. Практики валідації

- **Приклад 1: валідація в `set` властивості `Capacity`.** Не можна встановити місткість менше за кількість елементів, інакше вони б втратились. Для порушення викидається `ArgumentOutOfRangeException` (через допоміжний клас `ThrowHelper`).

```csharp
set
{
    if (value < _size)
    {
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value, ExceptionResource.ArgumentOutOfRange_SmallCapacity);
    }
    ...
}
```

- **Приклад 2: валідація в конструкторі.** Від'ємна місткість не дозволяється.

```csharp
public List(int capacity)
{
    if (capacity < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
    ...
}
```

- **Приклад 3: перевірка на null.** Методи, що приймають колекцію, перевіряють аргумент.

```csharp
public void AddRange(IEnumerable<T> collection)
{
    if (collection == null)
    {
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
    }
    ...
}
```

- **Приклад 4: перевірка стану в `Pop`.** Якщо стек порожній, викидається `InvalidOperationException` через допоміжний метод `ThrowForEmptyStack`.

```csharp
public T Pop()
{
    int size = _size - 1;
    T[] array = _array;

    if ((uint)size >= (uint)array.Length)
    {
        ThrowForEmptyStack();
    }

    _version++;
    _size = size;
    T item = array[size];
    ...
    return item;
}
```

- **Приклад 5: безпечна альтернатива без винятку.** Метод `TryPop` не викидає виняток, а повертає `false`, якщо стек порожній.

```csharp
public bool TryPop([MaybeNullWhen(false)] out T result)
{
    int size = _size - 1;
    T[] array = _array;

    if ((uint)size >= (uint)array.Length)
    {
        result = default!;
        return false;
    }
    ...
}
```

- **Висновки щодо валідації:** використовуються перевірки через `if` і винятки, а не атрибути з `DataAnnotations`. Це підходить для бібліотеки низького рівня, де важлива швидкість. Перевірка межі індексу записана в один порівняльний вираз, а виняток винесено в окремий допоміжний метод, щоб основний код був компактним. Окрім винятків, є й безпечні методи `TryPop` та `TryPeek`, які повертають `false` замість помилки, що дає вибір залежно від ситуації. Недолік у тому, що логіка розкидана по багатьох методах.

## 4. Загальні висновки

В усіх класах дотримано інкапсуляції: дані лежать у закритих полях, зовні доступні лише методи й властивості, а коректність гарантується перевірками. Для `Count` використано властивість тільки для читання, для `Capacity` у `List<T>` властивість із валідацією, для доступу за номером індексатор. Такий підхід захищає внутрішній масив від некоректних змін і дозволяє змінювати реалізацію без зміни коду, що користується класом.

## Відповіді на контрольні запитання

**1. За якими ознаками можна зробити висновок, що в класі дотримано інкапсуляції?**
Поля приховані, доступ іде через властивості й методи, є перевірка значень, а частина властивостей має лише `get`.

**2. Які підходи до валідації властивостей ви зустріли в реальному коді?**
Перевірки через `if` у `set` і в конструкторах, винятки `ArgumentOutOfRangeException`, `ArgumentNullException` та `InvalidOperationException`, допоміжний клас `ThrowHelper`, а також безпечні методи `TryPop` і `TryPeek`.

**3. Чому для аналізу важливо наводити посилання на конкретні класи та фрагменти коду?**
Щоб висновки можна було перевірити. Посилання й код показують, що аналіз зроблено на реальних прикладах, а не загальними словами.

**4. Які висновки щодо якості проєктування ви зробили після порівняння 2-3 класів?**
Усі класи мають схожу структуру: приватний масив і лічильник елементів та мінімальний публічний інтерфейс. Кожен клас відповідає за одну задачу, а внутрішня реалізація прихована від користувача.