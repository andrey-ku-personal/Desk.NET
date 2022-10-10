<template>
  <div class="field">
    <label class="label">{{ label }}</label>
    <p-textarea
      class="w-full"
      :class="validation && validation.$error ? 'p-invalid' : ''"
      :value="modelValue"
      :autoResize="autoResize"
      :rows="rows"
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
  name: "text-area",

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
    autoResize: {
      type: Boolean,
      required: true,
    },
    rows: {
      type: Number,
      required: false,
      default: 5,
    },
  },
});
</script>

<style scoped>
.field {
  display: flex;
  flex-direction: column;
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
