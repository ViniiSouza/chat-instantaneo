import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'

import Toast from '@/plugins/toast.js'
import "vue-toastification/dist/index.css"
import './assets/main.css'

const app = createApp(App)

app.use(router)

app.use(Toast);

app.mount('#app')
