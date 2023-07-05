import axios from '@/services/axios.js'

const BASE_ROUTE = 'conversation/'

export default {
  loadAll() {
    return axios.get(`${BASE_ROUTE}load-all`)
  },
  getConversation(id) {
    return axios.get(`${BASE_ROUTE}chat/${id}`)
  }
}