# handy-command

.Net Standard libary which implements ICommand interface and provides Command and AsyncCommand classes that are tipically used
to provide commanding behaviour to user interfaces.

This particular library does not rely on CommandManager which is part of WPF, therefore it can be used with Xamarin and Mono.

<h3>Usage: </h3>
<h4>1. Instantiating a command</h4>

```
 var command = new Command((obj) =>
            {
                // Do something
            }, 
            (obj) => 
            {
                //Function which evaluates if command can be executed
                return true; 
            });


            command.CanExecuteChanged += (sender, e) =>
            {
                //Do something on CanExecuteChanged
            };


            var asyncCommand = new AsyncCommand(async (obj) =>
            {
                // await something
            },(obj) => { return true; });
```
<h4>2. Raising CanExecuteChanged event</h4>

```
  command.RaiseCanExecuteChanged();
```
<h4>3. Executing command</h4>

```
  command.Execute(null);
  //For AsyncCommand
  await asyncCommand.ExecuteAsync(null);
```
<h4>4. Check if command can be executed</h4>

```
 if (command.CanExecute(null))
 {
     command.Execute(null);
 }
 if(asyncCommand.CanExecute(null))
 {
     await asyncCommand.ExecuteAsync(null);
 }
            
```

