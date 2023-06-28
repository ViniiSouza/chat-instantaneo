import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import Toast from 'vue-toastification'
import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'

import 'vue-toastification/dist/index.css'
import './assets/main.css'

const app = createApp(App)

app.use(router)

const options = {
  transition: 'Vue-Toastification__fade',
  timeout: 2500,
  closeOnClick: true,
  pauseOnFocusLoss: true,
  pauseOnHover: true,
  draggable: true,
  draggablePercent: 0.6,
  showCloseButtonOnHover: false,
  hideProgressBar: true,
  icon: true,
  maxToasts: 5,
  newestOnTop: true,
}

app.use(Toast, options)

app.mount('#app')
