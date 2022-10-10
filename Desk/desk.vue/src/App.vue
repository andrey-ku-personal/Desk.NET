<template>
  <div class="main-page">
    <sidebar-form></sidebar-form>
    <router-view :key="$route.fullPath"></router-view>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

import SidebarForm from "@/components/sidebar/SidebarForm.vue";

export default defineComponent({
  components: {
    SidebarForm,
  },
});
</script>

<style>
@font-face {
  font-family: "Ubuntu-Regular";
  src: local("Ubuntu-Regular"),
    url("@/assets/fonts/ubuntu/Ubuntu-Regular.ttf") format("truetype");
}

html {
  width: 100%;
  height: 100%;
  padding: 0;
  margin: 0;
}

body {
  margin: 0px;
  padding: 0px;
  color: black;
  font-family: "Ubuntu-Regular", sans-serif;
  display: flex;
  height: 100vh;
}

.main-page {
  display: flex;
  flex-direction: row;
}
</style>
