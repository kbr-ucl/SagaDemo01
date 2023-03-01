Sample Batch
============

This is a fork of: https://github.com/MassTransit/Sample-Batch

The code is located at: https://github.com/kbr-ucl/SampleBatch

This sample will show a variety of built in tools and techniques in MassTransit.

## Requirements ##

A message broker, and a database, and dotnet core. This sample provides a docker-compose.yml which uses RabbitMq (broker) and MsSql (db). If you are on windows, and have (LocalDb), and rabbitMq locally installed, you can skip the first step, but you will need to change the "ConnectionStrings" in the SampleBatch.Service appsettings.json.

## Easy Steps ##

1. start Docker Desktop
2. in a cmd prompt run the command `dotnet tool install --global dotnet-ef`
3. in the directory with the docker-compose.yml run the command `docker-compose up -d`
4. After complete, give it a few seconds, and you can browse to  http://localhost:15672, and view the rabbitmq management console
5. go into the src/SampleBatch.Service/ directory and type
   1. `dotnet ef database update`
   2. `dotnet run --console`
6. in another command window, go into src/SampleBatch.Api and type `dotnet run`
7. Browse to http://localhost:5000/swagger

## Play Around ##

Swagger provides a nice interface to see the api's available, and the models/query params for endpoints. It saves you the trouble of using
an additional tool like Postman, or fiddler to trigger the endpoints yourself.

So something I would do to start is go to the api/BatchJobs/create. Click "Try It Out", leave the defaults for now, and then click Execute.

It will make a BatchState saga, and spawn 100 jobs as part of the batch. But it will only have 10 jobs active and processing at a time.

You might want to try:
- put in a delay, and it will schedule the Batch to run in the future.
- cancel a delayed batch, before it starts
- cancel a delayed batch, after it starts

When you are all done, run `docker-compose down`

## Comments ##

My hope is to grow this sample over time, and introduce more documentation that walks through some of the decisions for example:

- I didn't want the DB Entities to have CorrelationId, even though the MassTransit ISaga requires the property be called that. Since MT v8, you no longer need to use a custom lock statement provider and formatter, because
  MT will pass along the column name to the formatter. So if you want to re-name "CorrelationId" to something else, all you need to do is add the column name in the entity configuration.
- I wanted to show, you can link state machines with children using SQL and Foreign Keys. This might not be the best design for your system. It all depends on the domain boundaries of your "microservice". It just shows one possibility, if you know your sagas are tightly coupled. In this sample, I want the user to be able to see a history of Batches that were ran and the status of the jobs it processed.
- The Sample allows RabbitMq or AzureSb to be used. All you do is add the config for the one you want, and on startup it will try to connect to the message broker that has config values present.
- I'd like to perhaps expand the db to create an actual orderId entity, where the Cancel and Suspend Activities would actually use a DBContext and cancel them. Also maybe introduce a couple routing slip activities in sequence, to then show compensation.
- Not too sure how I feel about the enum indicating what batch action to take, and then the switch statement in the ProcessJobConsumer where it conditionally creates the routingslip activity. Is there a more elegant way? I'll need to think about that.