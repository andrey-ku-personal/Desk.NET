<template>
  <router-view :key="$route.fullPath"></router-view>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({});
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
  color: #3d474d;
  font-family: "Ubuntu-Regular", sans-serif;
  display: flex;
  height: 100vh;
}
</style>
