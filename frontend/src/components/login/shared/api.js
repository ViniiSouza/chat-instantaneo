import axios from '@/services/axios.js'

const BASE_ROUTE = 'user/'

export default {
  signIn(username, password) {
    return axios.get(`${BASE_ROUTE}login?username=${username}&password=${password}`)
  }
}