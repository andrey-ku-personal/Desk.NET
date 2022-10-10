import { createRouter, createWebHashHistory } from "vue-router";
import HomePage from "@/pages/HomePage.vue";
import AgendaPage from "@/pages/AgendaPage.vue";
import ProjectPage from "@/pages/ProjectPage.vue";

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    { path: "/", name: "Home", component: HomePage },
    { path: "/Agenda", name: "Agenda", component: AgendaPage },
    { path: "/Project", name: "Project", component: ProjectPage },
  ],
});

export default router;
