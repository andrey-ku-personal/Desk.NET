import { createApp } from "vue";
import App from "@/App.vue";
import router from "@/router";

import directives from "@/directives/index";

import PrimeVue from "primevue/config";
import InputText from "primevue/inputtext";
import InputNumber from "primevue/inputnumber";
import Dialog from "primevue/dialog";
import Button from "primevue/button";
import Card from "primevue/card";
import Textarea from "primevue/textarea";
import VirtualScroller from "primevue/virtualscroller";
import RadioButton from "primevue/radiobutton";

import "primeflex/primeflex.css";
import "primevue/resources/themes/arya-orange/theme.css";
import "primevue/resources/primevue.min.css";
import "primeicons/primeicons.css";

import InputComponents from "@/components/input/index";
import TextAreaComponents from "@/components/textarea/index";

const app = createApp(App);

app.use(PrimeVue);
app.component("p-input-text", InputText);
app.component("p-input-number", InputNumber);
app.component("p-dialog", Dialog);
app.component("p-button", Button);
app.component("p-card", Card);
app.component("p-textarea", Textarea);
app.component("p-virtual-scroller", VirtualScroller);
app.component("p-radio-button", RadioButton);

InputComponents.forEach((component) => {
  app.component(component.name, component);
});

TextAreaComponents.forEach((component) => {
  app.component(component.name, component);
});

directives.forEach((directive) => {
  app.directive(directive.name, directive);
});

app.use(router);
app.mount("#app");
