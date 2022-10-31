<template>
  <div v-if="validation.$error">
    <div v-for="error of validation.$errors" :key="error.$uid">
      <div class="error-msg">{{ error.$message }}</div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "validation-message",

  props: {
    validation: {
      type: Object,
      required: false,
    },
  },
});
</script>

<style scoped>
.error-msg {
  font-size: 0.7em;
  color: darkred;
  font-weight: bold;
}
</style>
