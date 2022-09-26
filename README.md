# Demo Apache Kafka!

Para iniciar el servidor de prueba (un zookeper y un kafka):

    docker-compose -f .\docker\docker-compose.yml up -d 

Para detener el servidor:

    docker-compose -f .\docker\docker-compose.yml down

**Nota:** recordar que si se quiere empezar desde cero se debe borrar el volumen (carpeta demokafka dentro de la carpeta de ejecución)

# Interactuando con Kafka
Podemos interactuar con Kafka directamente desde nuestros ordenadores descargando [kafka](https://kafka.apache.org/downloads) y utilizando los compilados.

Para mostrar la lista de los topics disponibles:

    .\bin\windows\kafka-topics.bat --list --bootstrap-server localhost:9092

Para crear un topic:

    .\bin\windows\kafka-topics.bat --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic primertopic

Crear un consumidor para un topic:

    .\bin\windows\kafka-console-producer.bat --broker-list localhost:9092 --topic primertopic

Crear un consumidor para un topic:

    .\bin\windows\kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic primertopic --from-beginning
