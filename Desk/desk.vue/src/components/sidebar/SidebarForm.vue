<template>
  <div class="sidebar">
    <div class="sidebar-header">
      <img
        class="header-logo"
        src="@/assets/images/logo.svg"
        width="50"
        height="50"
      />
      <div class="header-title">
        <span>Task</span>
        <span>Desk</span>
      </div>
    </div>
    <div class="sidebar-links">
      <sidebar-link to="/" label="Dashboard" icon="pi pi-home"></sidebar-link>
      <sidebar-link to="/Agenda" label="Agenda" icon="pi pi-box"></sidebar-link>
      <project-sidebar-link
        to="/Project"
        label="Project"
        icon="pi pi-file"
      ></project-sidebar-link>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import SidebarLink from "@/components/sidebar/SidebarLink.vue";
import ProjectSidebarLink from "@/components/project/ProjectSidebarLink.vue";

export default defineComponent({
  name: "sidebar-form",

  components: {
    SidebarLink,
    ProjectSidebarLink,
  },

  data: function () {
    return {};
  },
});
</script>

<style scoped>
.sidebar {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;

  width: 320px;
  height: 100vh;

  background-color: #1e1e1e;
}

.sidebar-header {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;

  padding: 20px;
}

.sidebar-links {
  margin: 1em;
}

.content-slot {
  height: 100vh;
}

.header-title {
  display: flex;
  flex-direction: column;
  justify-content: center;

  margin-left: 10px;
}

.header-title span:first-child {
  font-size: 0.7em;
  color: white;
}

.header-title span:last-child {
  font-size: 1.3em;
  font-weight: 800;
  color: #ffd54f;
}
</style>
