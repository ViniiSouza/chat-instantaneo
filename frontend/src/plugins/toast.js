import Toast, { useToast } from 'vue-toastification';

const options = {
  transition: "Vue-Toastification__fade",
  timeout: 2500,
  closeOnClick: true,
  pauseOnFocusLoss: true,
  pauseOnHover: true,
  draggable: true,
  draggablePercent: 0.6,
  showCloseButtonOnHover: false,
  hideProgressBar: true,
  icon: true,
  maxToasts: 5,
  newestOnTop: true
}


export default {
  install: (app, opt) => {
    app.use(Toast, options);

    app.config.globalProperties.$toast = useToast()
  }
}