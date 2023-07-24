import axios from '@/services/axios.js'

const CHAT_BASE_ROUTE = 'conversation/'
const USER_BASE_ROUTE = 'user/'

export default {
  loadAll() {
    return axios.get(`${CHAT_BASE_ROUTE}load-all`)
  },
  getConversation(id) {
    return axios.get(`${CHAT_BASE_ROUTE}${id}`)
  },
  getContacts() {
    return axios.get(`${USER_BASE_ROUTE}contacts`)
  },
  removeContact(targetId) {
    return axios.delete(`${USER_BASE_ROUTE}contacts/${targetId}`)
  },
  searchUser(username) {
    return axios.get(`${USER_BASE_ROUTE}search?username=${username}`)
  },
  findPrivateConversation(targetUserName) {
    return axios.get(`${CHAT_BASE_ROUTE}simple/${targetUserName}`)
  },
  sendMessageRequest(messageInfo) {
    return axios.post(`${USER_BASE_ROUTE}request`, messageInfo)
  },
  sendMessage(message) {
    return axios.post(`${CHAT_BASE_ROUTE}message`, message)
  },
  createConversation(dto) {
    return axios.post(`${CHAT_BASE_ROUTE}create`, dto)
  },
  getRequestsFromUser() {
    return axios.get(`${USER_BASE_ROUTE}request/all`)
  }
}