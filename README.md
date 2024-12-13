# Desarrollo de Videojuegos I - Actividad 1
 

# Introducción
El juego que se presenta en este repositorio es un **space shooter 2D** que consiste en el control de una nave que tiene que destruir a los enemigos para ganar puntos.

# Mecánicas del juego
Los controles para el movimiento de la nave están configurados de la siguiente manera:
- Avanzar con las teclas W o flecha arriba.
- Retroceder con las teclas S o flecha abajo.
- Moverse lateralmente con las teclas A, D, flecha izquierda o flecha derecha.
- Disparar con la barra espaciadora.

# Aspectos de diseño, desarrollo e implementación
### Escenas
El juego cuenta con un total de 4 escenas:
1. **Menu:** donde se muestran los botones que permiten iniciar o salir del juego.
2. **Level_01:** contiene la información del primer nivel del juego.
3. **GameOver:** es la escena que se muestra una vez muere el jugador. Contiene botones para reiniciar, volver al menú inicial o salir.
4. **Win:** es la escena que se muestra jugador gana. Contiene botones para reiniciar, volver al menú inicial o salir.

### Personajes
#### **Player**
El objeto que controla el jugador no es más que un objeto que tiene como hijos a:
- **ShootPoint:** es un objeto que sirve como referencia para el disparo.
- **Anim:**  Un objeto que se usa para las animaciones.

Por otro lado, este objeto tiene enlazados los siguientes componentes:
- **BoxCollider2D:** se usa en su modo **trigger** para detectar las balas y los enemigos.
- **AudioSource:** para reproducir el sonido del disparo.
- **Player:** es un **script** para controlar el disparo y las colisiones.
- **PlayerMove:** es un **script** para controlar el movimiento del jugado teniendo en cuenta los límites de la pantalla.


#### **Enemy**
Solo hay un único tipo de enemigo y consiste en un **prefab** que contiene:
- **ShootPoint:** es un objeto que sirve como referencia para el disparo.
- **MoveAnim:**  Un objeto que se usa para las animaciones.


Por otro lado, este objeto tiene enlazados los siguientes componentes:
- **BoxCollider2D:** se usa en su modo **trigger** para detectar el choque con la nave y las balas del jugador.
- **AudioSource:** para reproducir el sonido del disparo.
- **Enemy:** es un **script** para controlar el disparo aleatorio, el movimiento aleatorio y las colosiones.

#### **Animaciones**

Las animaciones de la nave y de los enemigos se hicieron a través de un flip book descargado de la página Ichio.


### Otras clases y estructura

#### **Controllers**
Objetos y clases que permiten controlar un aspecto del juego:
- **GameController:** controla los puntos y las vidas del jugador. Además, imprime la información en UI y hace la transición a las escenas Win y GameOver según sea el caso.
- **SceneController:** crea los Boundaries que son los colliders que detectan cuando un enemigo o una bala esta fuera de pantalla y lo desactiva.
- **MusicController:** controla la música del Level_01.
- **TimeController:** es un controlador que lanza un evento cada vez que pasa cierto tiempo; por ejemplo, cada segundo. Se creó con el fin de no usar los distintos timer en el método Update.

#### **Spawners**
Objetos y clases que permiten instanciar o activar algún objecto:
- **EnemySpawner:** se encarga de instanciar o activar los enemigos cada tiempo aleatorio en una posición aleatoria. Para ello usa una **ObjectPool**.
- **BulletSpawner:** se encarga de instanciar o activar las balas según lo pida el jugador o los enemigos. Para ello usa distintos tipos de balas y distintos tipos de **ObjectPool**.
- **ExplosionSpawner:** se encarga de instanciar o activar explosiones según lo pida el jugador o los enemigos. Para ello usa un tipo de explosión y un tipo de **ObjectPool**.

#### **EventBuses**
Algunos aspectos de la programación del juego se basan en eventos, para ello se crearon una serie de buses o canales, con el fin de lograr un mayor desacoplamiento. Estos sirven de intermediarios entre la clase que lanza el evento y las clases que lo escuchan. Para ello se usaron **scriptable objects** los cuales son:
- **GameEventBusScrObj:** es el **ScriptableObject** que permite canalizar el lanzamiento y escucha de eventos, tales como la eliminación de un enemigo o la perdida de una vida del jugador.
- **TimeEventBusScrObj:** es el **ScriptableObject** que permite canalizar el lanzamiento y escucha de eventos de tiempo; por ejemplo, cada vez que pasa un segundo.


#### **Otros**
- **BackGround:** es un **GameObject** vacío que contiene los 4 fondos que se usaron para el efecto parallax. 
- **Parallax:** es un **script** que se añade a los fondos usados para crear el efecto parallax.

# Referencias
- Parallax (Forest, desert, sky, moon). Bongseng. https://bongseng.itch.io/el-bulleto-hello-rework
- Weapon, Gun, Laser, Scifi, Ray, Gunshot, Impact. PMSFX.  https://www.zapsplat.com/music/weapon-gun-laser-scifi-ray-gunshot-impact/\
- 2Cosmic Journey music pack. David KBD. https://davidkbd.itch.io/cosmic-journey-space-themed-music-pack
- Science fiction laser 2. ZapSplat. https://www.zapsplat.com/music/science-fiction-laser-2/
- Science fiction explosion with a whizz 5. ZapSplat. https://www.zapsplat.com/music/science-fiction-explosion-with-a-whizz-5/



