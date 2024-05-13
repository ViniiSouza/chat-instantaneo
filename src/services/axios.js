import axios from 'axios'
import router from '@/router/index.js'

const axiosInstance = axios.create({
  baseURL: 'http://localhost:5180',
})

axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      localStorage.removeItem('token')
      axiosInstance.interceptors.request.use((config) => {
        config.headers.Authorization = ''
        return config
      })
      router.push('/login')
    }

    return Promise.reject(error)
  }
)

export default axiosInstance
