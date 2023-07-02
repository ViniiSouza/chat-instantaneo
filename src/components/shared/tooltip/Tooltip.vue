<template>
  <div class="tooltip__container">
    <div class="tooltip__trigger">
      <slot name="component"> </slot>
    </div>
    <div class="tooltip__content">
      <slot name="content"> </slot>
    </div>
  </div>
</template>

<script>
import './shared/style.css'
</script>
