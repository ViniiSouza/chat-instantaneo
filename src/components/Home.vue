<template>
  <div class="d-flex justify-content-center">
    <div
      style="width: 500px"
      class="bg-light rounded mt-2"
    >
      <h2 class="text-center mt-2">Chat instantâneo</h2>
      <div class="text-center">
        <p>Selecione seu usuário:</p>
        <select
          id="usuario-select"
          v-model="loggedUser"
          name="select"
        >
          <option value="João">João</option>
          <option value="Maria">Maria</option>
          <option value="Pedro">Pedro</option>
        </select>
      </div>
      <div class="text-center my-4">
        <button
          class="btn btn-outline-info"
          @click="direcionarChat"
        >Ir ao chat</button>
      </div>
    </div>
  </div>
</template>
<style>
#usuario-select {
  width: 250px;
  border: 2px solid grey;
  border-radius: 5px;
}
</style>

<script>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import Swal from 'sweetalert2'

export default {
  setup () {
    const router = useRouter()

    //mudar aqui
    const loggedUser = ref()

    const direcionarChat = () => {
      if (loggedUser.value) {
        router.push({ name: 'chat', query: { user: loggedUser.value } })
      } else {
        Swal.fire({
          title: 'Erro',
          text: 'Selecione um usuário para continuar',
          timer: 2000,
          timerProgressBar: true,
          icon: 'error'
        })
      }
    }

    return {
      loggedUser,
      direcionarChat
    }
  }
}
</script>