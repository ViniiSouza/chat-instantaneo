<template>
  <div>
    <div class="d-flex justify-content-center">
      <div class="bg-light rounded px-4">
        <div class="text-center">
          <button
            class="btn btn-outline-info"
            @click="voltarHome"
          >Voltar ao menu principal</button>
        </div>
        <div class="text-center">
          <p>Selecione um usuário para iniciar uma conversa:</p>
          <select
            id="usuario-select"
            v-model="selectedUser"
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
            @click="criarChat"
          >Iniciar conversa</button>
        </div>
        <Chat
          v-for="(chat, index) in chats"
          :key="chat"
          :receiver-user="chat"
          :logged-user="loggedUser"
          @closeChat="closeChat(index)"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
import Chat from './Chat.vue'
import { useRoute, useRouter } from 'vue-router'
import Swal from 'sweetalert2'

export default {
  components: {
    Chat: Chat
  },
  setup () {
    const route = useRoute()
    const router = useRouter()

    const chats = ref([])

    const loggedUser = ref(route.query.user)
    const selectedUser = ref()

    const voltarHome = () => {
      router.push({ name: 'home' })
    }

    const criarChat = () => {
      if (chats.value.includes(selectedUser.value)) {
        Swal.fire({
          title: 'Erro',
          text: 'Já existe uma conversa com esse usuário',
          timer: 2000,
          timerProgressBar: true,
          icon: 'error'
        })
      } else if (loggedUser.value == selectedUser.value) {
        Swal.fire({
          title: 'Erro',
          text: 'Você não pode convesar com você mesmo',
          timer: 2000,
          timerProgressBar: true,
          icon: 'error'
        })
      }
      else {
        chats.value.push(selectedUser.value)
      }
    }

    const closeChat = index => {
      chats.value.splice(index, 1)
    }

    return {
      chats,
      loggedUser,
      selectedUser,
      voltarHome,
      criarChat,
      closeChat
    }
  }
}
</script>