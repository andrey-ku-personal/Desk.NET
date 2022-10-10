<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "sidebar-link",

  props: {
    to: { type: String, required: true },
    label: { type: String, required: true },
    icon: { type: String, required: false },
  },

  data: function () {
    return {};
  },

  methods: {
    navigateTo(): void {
      this.$router.push({ path: this.to });
    },
  },
});
</script>

<template>
  <p-button
    :label="label"
    class="p-button-text sidebar-link"
    :icon="icon"
    @click="navigateTo"
  />
</template>

<style scoped>
.sidebar-link {
  width: 100%;
  padding: 0.4em;
  text-align: left;
  margin: 2px 0;
}
</style>
