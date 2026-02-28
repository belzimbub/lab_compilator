# Лабораторная работа 1. Разработка пользовательского интерфейса (GUI) для языкового процессора

Цель: Создание кроссплатформенного графического интерфейса (GUI) для языкового процессора в виде специализированного текстового редактора.

Выполнил: Зимин Б. О.
Группа: АВТ-314
Факультет: АВТФ НГТУ

Описание проекта: Текстовый редактор, который в дальнейшем будет расширен до полноценного языкового процессора для анализа исходного кода.

Используемые технологии: C#, Windows Forms, Visual Studio 2022

## Инструкция по сборке и запуску:
- Выбрать Compiler Executable во вкладке Releases
- Скачать файл compiler.exe
- Программа готова к запуску

## Руководство пользователя:
На рисунке приведен пример рабочего окна текстового редактора.
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/399ca605-09ad-4d31-a574-9e590620c367" />

1 – основное меню программы;
2 – панель инструментов;
3 – окно/область ввода/редактирования текста;
4 – окно/область отображения результатов работы языкового процессора.

Панель инструментов содержит кнопки вызова часто используемых пунктов меню:
1) Создание документа
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/3a46077f-28d9-4730-8b90-1be29ad0aa4a" />

2) Открытие документа
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/82635b59-034a-45d1-9337-272b5ac1783e" />

3) Сохранение текущих изменений в документе
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/56f10c3d-33fe-470b-b242-352721245af9" />
После первого сохранения все изменения будут фиксироваться в заданном документе. Для сохранения документа в отдельный файл можно использовать функцию главного меню "Сохранить как":
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/f0b4affa-0c4c-41ca-b36e-3d22283143f8" />

4) Отмена изменений
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/12b8fffa-c4c9-4b6b-a5a0-382648ad4c9b" />

5) Повтор последнего изменения
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/5fbd5afc-2f90-4092-9a43-fad44d6a2b42" />

6) Вырезать текстовый фрагмент
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/206f47a5-113b-479a-ac67-8db9d3325830" />

7) Копировать текстовый фрагмент
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/3abef68a-54d3-40dc-b837-ee5140680334" />

8) Вставить текстовый фрагмент
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/5bf2e28f-8ba2-4071-aaf1-902499a17f40" />

10) Вызов справки - руководства пользователя
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/daaf7f66-c408-4ebf-b34e-fa0865adecde" />

11) Вызов информации о программе
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/d142ca5a-c924-4ff5-a0f1-4097bfb88c83" />

Главное меню программы имеет дополнительные функции:
12) Выход
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/4f691987-7f88-494b-a3e8-36b1b75bbbff" />

13) Удалить
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/0d8789c6-bc13-43f0-8089-5d81ef18c67d" />

14) Выделить все
<img width="986" height="758" alt="изображение" src="https://github.com/user-attachments/assets/4adf5c2a-0e2f-42c6-b900-215f582569dd" />


## Ограничения:
Возможность возникновения багов, связанных с функциями Отмены и Возврата изменений.
