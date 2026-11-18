
# Лабораторна робота №16

**Тема:** LINQ: SelectMany, пагінація й custom extensions

**Варіант:** 15

**Виконав:** Котяш Тарас

## Мета роботи

Навчитися використовувати SelectMany для роботи з вкладеними колекціями, реалізовувати пагінацію за допомогою Skip та Take, а також створювати власні методи розширення.

## Опис класів

У програмі створено три класи:

- Computer — комп'ютер, містить список компонентів.
- Component — компонент комп'ютера, містить список характеристик.
- Specification — характеристика компонента, містить ключ і значення.

Для тестування створено 4 комп'ютери, 12 компонентів та 25 характеристик.

## Виконані завдання

### 1. SelectMany

Використано SelectMany для отримання всіх характеристик усіх компонентів усіх комп'ютерів в одному списку.

### 2. Пагінація

Використано Skip() та Take() для виведення компонентів по 4 елементи на сторінку.

### 3. Custom Extension Method

Створено метод GetSpecification(string key), який шукає характеристику компонента за її ключем та повертає її значення.

### 4. Перевірка методу

Продемонстровано пошук характеристик Model, Memory та Cores.


## Приклад виводу програми

```text
=== LABORATORY WORK 16 ===
Computers and their components

=== 1. SELECT MANY ===
Model: Intel Core i7
Cores: 8
Frequency: 5.0 GHz
Model: RTX 4070
Memory: 12 GB
Capacity: 32 GB
Type: DDR5
Model: Intel Core i5
Cores: 6
Model: Intel UHD
Memory: Shared
Capacity: 512 GB
Type: NVMe
Model: AMD Ryzen 9
Cores: 16
Model: RTX 4080
Memory: 16 GB
Capacity: 64 GB
Type: DDR5
Model: AMD Ryzen 5
Cores: 6
Model: GTX 1660
Memory: 6 GB
Capacity: 256 GB
Type: SATA

Total specifications: 25

=== 2. PAGINATION ===

Page 1:
CPU
GPU
RAM
CPU

Page 2:
GPU
SSD
CPU
GPU

Page 3:
RAM
CPU
GPU
SSD

=== 3. CUSTOM EXTENSION METHOD ===

Computer: Gaming PC
CPU - Model: Intel Core i7
GPU - Model: RTX 4070
RAM - Model: Не знайдено

Computer: Office PC
CPU - Model: Intel Core i5
GPU - Model: Intel UHD
SSD - Model: Не знайдено

Computer: Workstation
CPU - Model: AMD Ryzen 9
GPU - Model: RTX 4080
RAM - Model: Не знайдено

Computer: Budget PC
CPU - Model: AMD Ryzen 5
GPU - Model: GTX 1660
SSD - Model: Не знайдено

=== 4. SEARCH SPECIFICATION ===
Component: CPU
Model: Intel Core i7
Memory: Не знайдено
Cores: 8

Program finished.
```


## Висновок

Під час лабораторної роботи було реалізовано роботу з вкладеними колекціями за допомогою SelectMany, пагінацію через Skip та Take, а також власний метод GetSpecification. Отримані результати показали, що LINQ спрощує обробку складних структур даних.

## Контрольні питання

### 1. Для чого використовується SelectMany?

SelectMany використовується для об'єднання вкладених колекцій в одну послідовність. На відміну від Select, який повертає вкладені колекції, SelectMany розгортає їх в один список.

### 2. Як реалізувати пагінацію за допомогою LINQ?

Для пагінації використовуються Skip() та Take(). Skip пропускає певну кількість елементів, а Take отримує потрібну кількість елементів.

### 3. Що таке методи розширення?

Методи розширення дозволяють додавати нові методи до існуючих типів без зміни їхнього вихідного коду.

### 4. Приклад власного методу розширення.

GetSpecification(string key) шукає характеристику компонента за ключем та повертає її значення. Якщо характеристику не знайдено, повертається повідомлення "Не знайдено".
