<template>
  <label class="toogle__switch">
    <input type="checkbox" v-model="checkbox" @click="toggleCheckbox" />
    <span class="toogle__slider round"></span>
  </label>
</template>
<script setup>
import './shared/style.css'
import { defineProps, ref, watch } from 'vue'

const checkbox = ref(false)

const props = defineProps({
  value: { type: Boolean, default: false },
})

checkbox.value = props.value

const emit = defineEmits(['setCheckboxVal'])

const toggleCheckbox = () => {
  checkbox.value = !checkbox.value
  emit('setCheckboxVal', checkbox.value)
}
</script>
