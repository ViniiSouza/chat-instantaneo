import { createRouter, createWebHistory } from 'vue-router'
import Home from '../components/Home.vue'
import Login from '../components/login/Login.vue'
import ChatContainer from '../components/Chat/ChatContainer.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/chat',
      name: 'chat',
      component: ChatContainer
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    }
  ]
})

export default router
