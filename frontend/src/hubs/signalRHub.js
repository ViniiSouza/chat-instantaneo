import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@aspnet/signalr'
import  { createApp } from 'vue'

export default {
  install (vue) {
    const connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl("https://localhost:7173/chat", {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();

    let startedPromise = null
    function start () {
      startedPromise = connection.start().catch(err => {
        console.error('Failed to connect with hub', err)
        return new Promise((resolve, reject) =>
          setTimeout(() => start().then(resolve).catch(reject), 5000))
      })
      return startedPromise
    }
    connection.onclose(() => start())

    // use new Vue instance as an event bus
    let app = createApp({})

    const questionHub = app
    // Forward server side SignalR events through $questionHub, where components will listen to them
    connection.on('QuestionScoreChange', (questionId, score) => {
    })
    
    questionHub.$emit('score-changed')
    start()


  }
}

// https://localhost:7173/chat