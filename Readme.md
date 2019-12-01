## Práctica Eventos

Esto es una aproximación al juego final para la asignatura. Se creado dos escenas una es la pantalla principal y la otra el juego.
 - En la pantalla principal, se ha realizado una UI muy básica, text box y buttons. 
 - En la del juego se puede acceder al menu para salir o reanudar el juego. Al empezar la pantalla se inicia el tutorial el cual esta pensado
    de tal forma de que medida que el jugador se familiarize con las mecáninas se vayan cambiando el texto. El boton skip permite saltarse, el tutorial.
    Este tutorial de texto esta diseñado para ser escrito en XML (Dialogs/Tutorial.xml). Aún está en proceso de implementación.

Hay un gif con el uso de la UI. La parte de diálogo es saltada manualmente debido a que es necesario crear eventos dentro del juego que desencaden los distintos diálogos en los momentos oportunos.

![](somebasicguisample.gif)

Para la parte de los eventos nativos de c# se ha implementado una clase FleeBehaviour la cual se asocia a cada ratón de tal forma que cuando un raton de un grupo huya todos los de ese mismo grupo huyan también. (Actualmente está de forma muy básica, solo hay un grupo, pero la idea es formar grupos según la distancia a otros ratones).

La clase FleeBehaviour declara un evento estático para compartirlo entre las distintas instancias.

![](game.gif)