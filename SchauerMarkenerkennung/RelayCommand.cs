﻿using System;
using System.Windows.Input;

namespace MvvmTools;

public class RelayCommand<T> : ICommand
{
    private readonly Action<T?> _execute;
    private readonly Predicate<T?>? _canExecute;

    public RelayCommand(Action<T?> execute) : this(execute, null)
    { }

    public RelayCommand(Action<T?> execute, Predicate<T?>? canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public void Execute(object? parameter)
    {
        if (parameter == null) _execute(default);
        else _execute((T)parameter);
    }
    public bool CanExecute(object? parameter) =>
                  _canExecute == null
              || (parameter == null ? _canExecute(default) : _canExecute((T)parameter));

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

}