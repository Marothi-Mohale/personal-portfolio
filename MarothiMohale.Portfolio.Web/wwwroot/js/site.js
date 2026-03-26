(() => {
  const root = document.documentElement;
  const savedTheme = localStorage.getItem("portfolio-theme");
  if (savedTheme) {
    root.dataset.theme = savedTheme;
  }

  document.querySelector("[data-theme-toggle]")?.addEventListener("click", () => {
    const nextTheme = root.dataset.theme === "light" ? "dark" : "light";
    root.dataset.theme = nextTheme;
    localStorage.setItem("portfolio-theme", nextTheme);
  });

  const nav = document.querySelector(".site-nav");
  document.querySelector("[data-nav-toggle]")?.addEventListener("click", (event) => {
    const button = event.currentTarget;
    const isOpen = nav?.classList.toggle("is-open");
    button?.setAttribute("aria-expanded", String(Boolean(isOpen)));
  });
})();
