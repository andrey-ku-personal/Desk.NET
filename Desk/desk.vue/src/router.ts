import { createRouter, createWebHashHistory } from "vue-router";
import HomePage from "@/pages/HomePage.vue";
import AgendaPage from "@/pages/AgendaPage.vue";
import ProjectPage from "@/pages/ProjectPage.vue";
import SignUpPage from "@/pages/SignUpPage.vue";

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    { path: "/", name: "Home", component: HomePage },
    { path: "/Agenda", name: "Agenda", component: AgendaPage },
    { path: "/Project", name: "Project", component: ProjectPage },
    { path: "/SignUp", name: "Project", component: SignUpPage },
  ],
});

export default router;
