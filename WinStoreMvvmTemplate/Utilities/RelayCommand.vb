Imports System.Windows.Input

Namespace Utilities

    ''' <summary>
    '''  A command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    ''' </summary>
    ''' <remarks>The default return value for the CanExecute method is <code>True</code>.</remarks>
    Public Class RelayCommand : Implements ICommand

#Region " Fields                        "

        ReadOnly _execute As Action(Of Object)
        ReadOnly _canExecute As Predicate(Of Object)

#End Region

#Region " Constructors                  "

        ''' <summary>
        ''' Creates a new command that can always execute.
        ''' </summary>
        ''' <param name="execute">The execution logic.</param>
        ''' <remarks></remarks>
        Public Sub New(execute As Action(Of Object))
            Me.New(execute, Nothing)
        End Sub

        ''' <summary>
        ''' Creates a new command.
        ''' </summary>
        ''' <param name="execute">The execution logic.</param>
        ''' <param name="canExecute">The execution status logic.</param>
        ''' <remarks></remarks>
        Public Sub New(execute As Action(Of Object), canExecute As Predicate(Of Object))
            If execute Is Nothing Then Throw New ArgumentNullException("execute")
            _execute = execute
            _canExecute = canExecute
        End Sub

#End Region

#Region " ICommand Members              "

        ''' <summary>
        ''' Sets Can Execute Property to True or False
        ''' </summary>
        ''' <param name="parameter"></param>
        ''' <returns>Whether or not the action can execute</returns>
        ''' <remarks>
        ''' Uses <see cref="System.Diagnostics.DebuggerStepThroughAttribute"/>
        ''' CanExecute is called everytime the UI checks if it CanExecute something.
        ''' Debugger will step over unless there is an explicit break point inside of it
        ''' </remarks>
        <DebuggerStepThrough()>
        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return _canExecute Is Nothing OrElse _canExecute(parameter)
        End Function

        ''' <summary>
        ''' Listener when CanExecute Property Changes
        ''' </summary>
        ''' <remarks></remarks>
        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        ''' <summary>
        ''' Launches Execute ICommand
        ''' </summary>
        ''' <param name="parameter"></param>
        ''' <remarks></remarks>
        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            _execute(parameter)
        End Sub

#End Region

    End Class

End Namespace
