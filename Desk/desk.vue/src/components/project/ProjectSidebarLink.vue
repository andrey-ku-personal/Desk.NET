<script lang="ts">
import { defineComponent } from "vue";
import { BaseHttp } from "@/Helpers/BaseHttp";
import ProjectCreatePopup from "@/components/project/ProjectCreatePopup.vue";
import { ProjectModel } from "@/components/project/models/projectModel";
import { GetAllRequest } from "@/components/project/requests/getAllRequest";
import { FilterResult } from "@/shared/filter/models/filterResult";

export default defineComponent({
  name: "project-sidebar-link",

  components: {
    ProjectCreatePopup,
  },

  props: {
    to: { type: String, required: true },
    label: { type: String, required: true },
    icon: { type: String, required: false },
  },

  data: function () {
    return {
      listVisible: false,
      rotateClass: "",
      filter: {
        pageNumber: 0,
        pageSize: -1,
        SortBy: "name",
        isAscending: true,
        SearchByText: "",
      } as GetAllRequest,
      projects: [] as ProjectModel[],

      isCreationMenuDisplayed: false,
      isCreatePopupDisplayed: false,

      isStatusMenuDisplayed: false,
    };
  },

  methods: {
    async changeListVisability(): Promise<void> {
      this.listVisible = !this.listVisible;

      this.rotateClass === "rotate-up"
        ? (this.rotateClass = "rotate-down")
        : (this.rotateClass = "rotate-up");

      if (this.listVisible) await this.Load();
    },

    hideCreationMenu() {
      this.isCreationMenuDisplayed = false;
    },

    openPopup() {
      this.isCreationMenuDisplayed = false;
      this.isStatusMenuDisplayed = false;
    },

    closePopup() {
      this.isCreatePopupDisplayed = false;
    },

    hideStatusMenu() {
      this.isStatusMenuDisplayed = false;
    },

    async updateList(): Promise<void> {
      if (this.listVisible) await this.Load();
    },

    onSearch(event: Event) {
      this.filter.SearchByText = (event.target as HTMLInputElement).value;
      this.Load();
    },

    async Load(): Promise<void> {
      try {
        const response = await BaseHttp.post("project/GetAll", this.filter);

        if (response.status === 200) {
          const result = response.data as FilterResult<ProjectModel>;
          this.projects = result.result;
        } else {
          this.projects = [];
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
});
</script>

<template>
  <div class="project-link">
    <p-button @click="changeListVisability" class="p-button-text sidebar-link">
      <div class="link-name">
        <i :class="icon"></i>
        <span>{{ label }}</span>
      </div>
      <transition name="list">
        <i class="pi pi-angle-left" :class="rotateClass"></i>
      </transition>
    </p-button>

    <div class="project-menu">
      <div v-click-outside="hideStatusMenu">
        <p-button
          id="button"
          class="p-button-rounded p-button-text icon-menu-btn"
          @click="isStatusMenuDisplayed = !isStatusMenuDisplayed"
        >
          <i class="pi pi-ellipsis-h"></i>
        </p-button>

        <transition name="menu-popup">
          <div v-show="isStatusMenuDisplayed" class="menu-popup">
            <p-button class="p-button-text p-button-secondary menu-button">
              <div class="link-name">
                <i class="pi pi-file"></i>
                <span>Active Projects</span>
              </div>
            </p-button>

            <p-button class="p-button-text p-button-secondary menu-button">
              <div class="link-name">
                <i class="pi pi-box"></i>
                <span>Archived Projects</span>
              </div>
            </p-button>

            <p-button class="p-button-text p-button-secondary menu-button">
              <div class="link-name">
                <i class="pi pi-trash"></i>
                <span>Deleted Projects</span>
              </div>
            </p-button>
          </div>
        </transition>
      </div>

      <div v-click-outside="hideCreationMenu">
        <p-button
          id="button"
          class="p-button-rounded p-button-text icon-menu-btn"
          @click="isCreationMenuDisplayed = !isCreationMenuDisplayed"
        >
          <i class="pi pi-plus-circle"></i>
        </p-button>

        <transition name="menu-popup">
          <div v-show="isCreationMenuDisplayed" class="menu-popup">
            <p-button
              class="p-button-text p-button-secondary menu-button"
              @click="isCreatePopupDisplayed = !isCreatePopupDisplayed"
            >
              <div class="link-name">
                <i class="pi pi-plus-circle"></i>
                <span>New Project...</span>
              </div>
            </p-button>
            <project-create-popup
              @open="openPopup"
              @close="closePopup"
              @created="updateList"
              :visible="isCreatePopupDisplayed"
            />

            <p-button class="p-button-text p-button-secondary menu-button">
              <div class="link-name">
                <i class="pi pi-folder"></i>
                <span>New Project Group...</span>
              </div>
            </p-button>

            <p-button class="p-button-text p-button-secondary menu-button">
              <div class="link-name">
                <i class="pi pi-upload"></i>
                <span>Import...</span>
              </div>
            </p-button>
          </div>
        </transition>
      </div>
    </div>
  </div>

  <transition name="project-list">
    <div v-if="listVisible" class="project-list">
      <p-input-text
        class="search-field"
        placeholder="Search by:"
        v-model.trim="filter.SearchByText"
        @Change="onSearch($event)"
      />

      <p-virtual-scroller :items="projects" :itemSize="39">
        <template v-slot:item="{ item, options }">
          <div :class="['scroll-item p-1', { odd: options.odd }]">
            <p-button
              id="button"
              class="p-button-text p-button-sm project-item"
            >
              <span>{{ item.name }}</span>
            </p-button>
          </div>
        </template>
      </p-virtual-scroller>
    </div>
  </transition>
</template>

<style scoped>
.project-link {
  position: relative;
}

.project-menu {
  position: absolute;
  right: 30px;
  bottom: 5px;

  display: flex;
  flex-direction: row;
}

.sidebar-link {
  width: 100%;
  padding: 0.4em;
  text-align: left;
  margin: 2px 0;

  display: flex;
  flex-direction: row;
  justify-content: space-between;
  position: relative;
}

.link-name i {
  margin-right: 8px;
}

.icon-menu-btn {
  padding: 4px;
  margin-right: 4px;
  position: relative;
}

@keyframes rotateDown {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(-90deg);
  }
}
@keyframes rotateUp {
  from {
    transform: rotate(-90deg);
  }
  to {
    transform: rotate(0deg);
  }
}
.rotate-down {
  animation: rotateDown 0.5s ease 0s 1 reverse forwards;
}
.rotate-up {
  animation: rotateUp 0.5s ease 0s 1 reverse forwards;
}

.menu-popup {
  position: absolute;
  left: 50%;
  top: 50px;
  transform: translateX(-50%);
  width: 300px;
  background-color: white;
  border-radius: 15px;
  box-shadow: 1px 1px 15px 0 rgba(0, 0, 0, 0.2);
  color: #555585;
  padding: 10px;

  display: flex;
  flex-direction: column;
  z-index: 999;
}

.menu-button {
  width: 100%;
  text-align: left;
}

.menu-popup-enter-active {
  transition: opacity 0.5s ease;
}

.menu-popup-leave-active {
  transition: opacity 0.1s ease;
}

.menu-popup-enter-from,
.menu-popup-leave-to {
  opacity: 0;
}

.project-list {
  height: 300px;
}

.project-list-enter-active,
.project-list-leave-active {
  transition: opacity 0.5s ease;
}

.project-list-enter-from,
.project-list-leave-to {
  opacity: 0;
}

.search-field {
  width: 100%;
  margin: 10px 0px;
}

.project-item {
  width: 100%;
  color: #ffd54f;
  margin: 0;
}
</style>
