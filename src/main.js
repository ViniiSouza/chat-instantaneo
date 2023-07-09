import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import Toast from 'vue-toastification'
import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'

import { library } from "@fortawesome/fontawesome-svg-core";
import { faUserPlus, faGear, faPenToSquare, faMagnifyingGlass, faArrowUpRightFromSquare } from "@fortawesome/free-solid-svg-icons";
import { faCircleQuestion, faPaperPlane, faSquareMinus } from "@fortawesome/free-regular-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import 'vue-toastification/dist/index.css'
import './assets/main.css'

const app = createApp(App)

library.add(faUserPlus)
library.add(faGear)
library.add(faPenToSquare)
library.add(faCircleQuestion)
library.add(faPaperPlane)
library.add(faSquareMinus)
library.add(faMagnifyingGlass)
library.add(faArrowUpRightFromSquare)

app.component("font-awesome-icon", FontAwesomeIcon)

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
