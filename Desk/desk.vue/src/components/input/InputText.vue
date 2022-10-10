<template>
  <div class="field">
    <label class="label">{{ label }}</label>
    <p-input-text
      type="text"
      class="w-full"
      :class="validation && validation.$error ? 'p-invalid' : ''"
      :value="modelValue"
      :placeholder="placeholder"
      @input="$emit('update:modelValue', $event.target.value)"
    />
    <div v-if="validation && validation.$error">
      <div v-for="error of validation.$errors" :key="error.$uid">
        <div class="error-msg">{{ error.$message }}</div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "input-text",

  props: {
    label: {
      type: String,
      required: true,
    },
    placeholder: {
      type: String,
      required: false,
      default: "",
    },
    modelValue: [String],
    validation: {
      type: Object,
      required: false,
    },
  },
});
</script>

<style scoped>
.field {
  display: flex;
  flex-direction: column;
  width: 100%;
}

.label {
  font-size: 0.8em;
  margin-bottom: 0;
}

.error-msg {
  font-family: "Ubuntu-Regular", sans-serif;
  font-size: 0.7em;
  color: darkred;
  font-weight: bold;
}
</style>
