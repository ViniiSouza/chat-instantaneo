import axios from '@/services/axios.js'

const BASE_ROUTE = 'user/'

export default {
  signUp(user) {
    return axios.post(`${BASE_ROUTE}new`, user)
  }
}