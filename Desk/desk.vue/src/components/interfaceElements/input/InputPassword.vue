<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "input-password",

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
    validation: {
      type: Object,
      required: false,
    },
  },

  data: function () {
    return {
      password: "" as string,
    };
  },
});
</script>

<template>
  <div class="field">
    <label class="label">{{ label }}</label>
    <p-password
      type="text"
      toggleMask="true"
      class="password"
      :class="validation && validation.$error ? 'p-invalid' : ''"
      v-model="password"
      :placeholder="placeholder"
      @input="$emit('update:password', password)"
    />
    <div v-if="validation && validation.$error">
      <div v-for="error of validation.$errors" :key="error.$uid">
        <div class="error-msg">{{ error.$message }}</div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.field {
  display: flex;
  flex-direction: column;
  width: 100%;
}

.label {
  font-size: 0.8em;
  margin-bottom: 5px;
}

.error-msg {
  font-family: "Ubuntu-Regular", sans-serif;
  font-size: 0.7em;
  color: darkred;
  font-weight: bold;
}

.password {
  padding-right: 2.1rem !important;
}
</style>
