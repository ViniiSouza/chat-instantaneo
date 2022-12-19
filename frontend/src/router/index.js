import { createRouter, createWebHistory } from 'vue-router'
import Home from '../components/Home.vue'
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
  ]
})

export default router
