<template>
  <div v-if="showConfig" class="options__container">
    <p class="options__item" v-for="item in items" :key="item.name" @click="item.event">{{ item.name }}</p>
  </div>
</template>
<script setup>
const props = defineProps({
  items: {
    type: Array,
    default: []
  },
  showConfig: {
    type: Boolean,
    default: false
  }
})
</script>
<style scoped>
.options__container {
  position: absolute;
  right: 5px;
  transform: translateY(5px);
  background-color: var(--white);
  min-width: 120px;
  border-radius: 5px;
  z-index: 1050;
}

.options__item {
  font-size: 16px;
  margin-bottom: 5px;
  color: var(--dark);
  display: block;
  padding: 5px 12px;
  cursor: pointer;
}

.options__item:hover {
  background-color: var(--grey);
}

</style>