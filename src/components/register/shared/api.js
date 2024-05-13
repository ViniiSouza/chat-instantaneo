import axios from '@/services/axios.js'

const BASE_ROUTE = 'authentication/'

export default {
  signUp(user) {
    return axios.post(`${BASE_ROUTE}register`, user)
  }
}