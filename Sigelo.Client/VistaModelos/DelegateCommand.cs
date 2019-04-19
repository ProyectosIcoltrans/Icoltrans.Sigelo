using System;
using System.Windows.Input;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Delegado para estandarizar el llamado en los Binding de tipo <see cref="ICommand"/> 
    /// </summary>
    /// <remarks><code>
    /// *-----------------------------------------------------------------------------------------*
    /// * Copyright (C) 2008 Michael Guerrero & Cia Ltda., Todos los Derechos Reservados
    /// * http://www.cnt.com.co - mailto:produccion_panacea@cnt.com.co
    /// *
    /// * Archivo:      DelegateCommand.cs
    /// * Tipo:         Clase de Apoyo al XAML
    /// * Autor:        Jaimir Guerrero
    /// * Fecha:        2008 Mar 03
    /// * Propósito:    Delegado para estandarizar el llamado en los Binding de tipo <see cref="ICommand"/> 
    /// *-----------------------------------------------------------------------------------------*
    /// </code></remarks>
    public class DelegateCommand : ICommand
    {
        Func<object, bool> canExecute;
        Action<object> executeAction;
        bool canExecuteCache;

        /// <summary>
        /// Inicializa una nueva instancia de la class <see cref="DelegateCommand"/>.
        /// </summary>
        /// <param name="executeAction">La acción a ejecutar al llamar el comando.</param>
        /// <param name="canExecute">La función que verifica si se puede ejecutar el comando.</param>
        /// 25/08/2010 by Jaimir Guerrero
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        #region ICommand Members
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// 25/08/2010 by Jaimir Guerrero
        public bool CanExecute(object parameter)
        {
            bool temp = canExecute(parameter);
            if (canExecuteCache != temp)
            {
                canExecuteCache = temp;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, new EventArgs());
            }
            return canExecuteCache;
        }
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        /// 25/08/2010 by Jaimir Guerrero
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// 25/08/2010 by Jaimir Guerrero
        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
        #endregion
    }
}
