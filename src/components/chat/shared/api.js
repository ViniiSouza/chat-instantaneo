import axios from '@/services/axios.js'

const CHAT_BASE_ROUTE = 'conversation/'
const USER_BASE_ROUTE = 'user/'

export default {
  loadAll() {
    return axios.get(`${CHAT_BASE_ROUTE}load-all`)
  },
  getConversation(id) {
    return axios.get(`${CHAT_BASE_ROUTE}chat/${id}`)
  },
  getContacts() {
    return axios.get(`${USER_BASE_ROUTE}contacts`)
  },
  removeContact(targetId) {
    return axios.delete(`${USER_BASE_ROUTE}contacts/${targetId}`)
  }
}