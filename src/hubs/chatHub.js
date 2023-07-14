import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@aspnet/signalr'

export default (() => {
    const accessToken = localStorage.getItem('token')

    if (!accessToken) {
      return
    }

    const connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl(`https://localhost:7180/hubs/chat?access_token=${accessToken}`, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();
      
    const fulfilled = () => {
      // do something on connection start
    }

    const rejected = () => {
      // do something on connection reject
    }

    let startedPromise = null

    const start = () => {
      startedPromise = connection.start().catch(err => {
        console.error('Failed to connect with hub', err)
        return new Promise((resolve, reject) =>
          setTimeout(() => start().then(resolve).catch(reject), 5000))
      })
    }

    connection.onclose((err) =>  {
      if (err.message.includes('Unauthorized connection') // backend default message when not authorized
      || err.message.includes('connections limit exceeded')) { // backend default message when user exceeded concurrent connections
        return
      }
      // need to handle this in the future
      start()
    })
    
    start()

    return connection
})
