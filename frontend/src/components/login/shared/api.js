import axios from '@/services/axios.js'

const BASE_ROUTE = 'authentication/'

export default {
  signIn(user) {
    return axios.post(`${BASE_ROUTE}login`, user)
  }
}