import api from './shared/api'
import chatHub from '@/hubs/chatHub.js'

export default function useChat(
  router,
  toast,
  currentChat,
  hasDraft,
  conversations,
  modalTitle,
  showModal,
  currentModal,
  onBeforeUnmount,
  ChatAreaCp,
  InviteCp,
  NotificationSoundEl,
) {
  // hub

  const hub = chatHub()

  hub.on('MessageReceived', (response) => {
    NotificationSoundEl.value.play()
    let scrollOnReceive = ChatAreaCp.value.userInBottom

    let index = conversations.value.findIndex(
      (find) => find.id == response.conversationId
    )
    if (index >= 0) {
      conversations.value[index].lastMessage = response
    }

    if (currentChat.value && currentChat.value.id == response.conversationId) {
      currentChat.value.messages.push(response)
      if (scrollOnReceive) ChatAreaCp.value.scrollToBottom()
    }
  })

  hub.on('RequestReceived', () => {
    toast.info('You received a new message request')
  })

  hub.on('UserStatusChange', (response) => {
    updateConversationStatus(
      response,
      currentChat.value.status?.userName == response.userName
    )
  })

  // before unmount, close the connection...
  onBeforeUnmount(() => {
    hub.stop()
  })

  const openMenuOption = (optionValue) => {
    showModal.value = true
    currentModal.value = optionValue
    switch (optionValue) {
      case 1:
        modalTitle.value = 'Contacts'
        break
      case 2:
        modalTitle.value = 'Invite user'
        break
    }
  }

  const loadAllConversations = () => {
    api
      .loadAll()
      .then((response) => {
        if (response.status == 200) {
          conversations.value = response.data
        }
      })
      .catch((err) => {
        if (err.response && err.response.data) {
          toast.error(err.response.data)
        } else {
          const errorMsg = 'Unable to load your conversations. Try again later.'
          toast.error(errorMsg)
        }
      })
  }

  const updateConversationStatus = (statusInfo, isCurrentChat = false) => {
    if (isCurrentChat || currentChat.value.receiver == statusInfo.userName) {
      currentChat.value.status = statusInfo
    }
  }

  const loadConversation = (conversation) => {
    if (!currentChat.value || currentChat.value.id != conversation.id) {
      removeDraft()
      if (conversation.draft) {
        currentChat.value = {
          messages: [],
          title: conversation.title,
          type: 1,
          draft: true,
          receiver: conversation.receiver,
          id: conversation.id,
        }
        hub
          .invoke('ListenToUserStatus', conversation.receiver)
          .then((result) => {
            updateConversationStatus(result, true)
          })
      } else {
        api
          .getConversation(conversation.id)
          .then((payload) => {
            let result = payload.data
            if (!result.title) result.title = conversation.title
            currentChat.value = result
            ChatAreaCp.value.scrollToBottom()
            if (result.type == 1) {
              hub
                .invoke('ListenToUserStatusByPrivateChat', conversation.id)
                .then((result) => {
                  updateConversationStatus(result, true)
                })
            }
          })
          .catch((err) => {
            if (err.response && err.response.data) {
              toast.error(err.response.data)
            } else {
              const errorMsg =
                'Unable to load this conversation. Try again later.'
              toast.error(errorMsg)
            }
          })
      }
    }
  }

  const createTempChat = (user) => {
    removeDraft()
    const tempChat = {
      id: -1,
      lastMessage: {
        content: 'Draft',
        ownMessage: false,
      },
      title: user.name,
      type: 1,
      draft: true,
      receiver: user.userName,
    }
    conversations.value.unshift(tempChat)
    loadConversation(conversations.value[0])
    hasDraft.value = true
    showModal.value = false
  }

  const removeDraft = () => {
    if (hasDraft.value) {
      conversations.value.shift()
      hasDraft.value = false
    }
  }

  const sendMessageToContact = (target) => {
    api
      .findPrivateConversation(target.userName)
      .then((payload) => {
        if (payload.status == 200) {
          loadConversation(payload.data)
          showModal.value = false
        }
      })
      .catch((err) => {
        if (err.response.status == 404) {
          createTempChat(target)
        } else {
          const errorMsg = 'Something went wrong. Try again later.'
          toast.error(errorMsg)
        }
      })
  }

  const sendMessageFromChat = (message) => {
    if (currentChat.value.draft) {
      const conversationObj = {
        receiver: currentChat.value.receiver,
        firstMessage: message,
        type: 1, // only private for now
      }
      hub.invoke('CreateConversation', conversationObj).then((result) => {
        loadConversation(result)
        conversations.value.unshift(result)
      })
    } else {
      const handlerId = Date.now()
      const messageObj = {
        id: handlerId,
        ownMessage: true,
        sendingTime: new Date(),
        content: message,
        action: 1,
        conversationId: currentChat.value.id,
      }
      currentChat.value.messages.push(messageObj)
      messageObj.sendingTime = messageObj.sendingTime.toJSON()
      hub.invoke('SendMessage', messageObj).then((result) => {
        let index = currentChat.value.messages.findIndex(
          (where) => where.id == handlerId
        )
        if (index >= 0) {
          currentChat.value.messages[index] = result
          ChatAreaCp.value.scrollToBottom()
        }
        let chatIndex = conversations.value.findIndex(
          (find) => find.id == result.conversationId
        )
        if (chatIndex >= 0) {
          conversations.value[chatIndex].lastMessage = result
        }
      })
    }
  }

  const openPrivateChat = (target) => {
    api
      .findPrivateConversation(target.userName)
      .then((payload) => {
        if (payload.status == 200) {
          loadConversation(payload.data)
          showModal.value = false
        }
      })
      .catch((err) => {
        if (err.response.status == 404) {
          createTempChat(target)
        } else if (err.response && err.response.data) {
          toast.error(err.response.data)
        } else {
          const errorMsg = 'Unable to find this conversation. Try again later.'
          toast.error(errorMsg)
        }
      })
  }

  const clearCurrentChat = () => {
    currentChat.value = null
  }

  const logout = () => {
    router.push({ name: 'login' })
  }

  const acceptRequest = (requestId) => {
    api.acceptRequest(requestId).then((payload) => {
      loadConversation(payload.data)
      showModal.value = false
      conversations.value.unshift(payload.data)
    })
  }

  const refuseRequest = (requestId) => {
    api.refuseRequest(requestId).then(() => {
      InviteCp.value.reloadRequests()
      toast.success('The request was deleted!')
    })
  }

  // on each first load
  loadAllConversations()

  return {
    openMenuOption,
    createTempChat,
    loadConversation,
    sendMessageToContact,
    sendMessageFromChat,
    openPrivateChat,
    logout,
    clearCurrentChat,
    acceptRequest,
    refuseRequest,
  }
}
