// Portfolio Core Client-Side Logic
document.addEventListener('DOMContentLoaded', () => {
  initTheme();
  initProjectFilter();
  initContactForm();
  initSmoothScroll();
  initSkillAnimations();
});

// Theme Management
function initTheme() {
  const themeBtn = document.getElementById('themeToggleBtn');
  const icon = themeBtn ? themeBtn.querySelector('i') : null;
  
  const savedTheme = localStorage.getItem('portfolio-theme') || 'dark';
  document.documentElement.setAttribute('data-theme', savedTheme);
  updateThemeIcon(savedTheme, icon);

  if (themeBtn) {
    themeBtn.addEventListener('click', () => {
      const currentTheme = document.documentElement.getAttribute('data-theme');
      const newTheme = currentTheme === 'light' ? 'dark' : 'light';
      document.documentElement.setAttribute('data-theme', newTheme);
      localStorage.setItem('portfolio-theme', newTheme);
      updateThemeIcon(newTheme, icon);
    });
  }
}

function updateThemeIcon(theme, icon) {
  if (!icon) return;
  if (theme === 'light') {
    icon.className = 'fa-solid fa-moon';
  } else {
    icon.className = 'fa-solid fa-sun';
  }
}

// Interactive Project Filter System
function initProjectFilter() {
  const filterBtns = document.querySelectorAll('.filter-btn');
  const projectCards = document.querySelectorAll('.project-card');

  filterBtns.forEach(btn => {
    btn.addEventListener('click', () => {
      filterBtns.forEach(b => b.classList.remove('active'));
      btn.classList.add('active');

      const category = btn.getAttribute('data-filter');

      projectCards.forEach(card => {
        const cardCategory = card.getAttribute('data-category');
        if (category === 'all' || cardCategory === category) {
          card.style.display = 'flex';
          setTimeout(() => {
            card.style.opacity = '1';
            card.style.transform = 'scale(1)';
          }, 50);
        } else {
          card.style.opacity = '0';
          card.style.transform = 'scale(0.95)';
          setTimeout(() => {
            card.style.display = 'none';
          }, 300);
        }
      });
    });
  });
}

// AJAX Contact Form Handling
function initContactForm() {
  const form = document.getElementById('contactForm');
  const alertToast = document.getElementById('formAlert');

  if (!form) return;

  form.addEventListener('submit', async (e) => {
    e.preventDefault();
    
    const submitBtn = form.querySelector('button[type="submit"]');
    const originalText = submitBtn.innerHTML;

    const data = {
      fullName: document.getElementById('FullName').value,
      email: document.getElementById('Email').value,
      subject: document.getElementById('Subject').value,
      message: document.getElementById('Message').value
    };

    // Client validation
    if (!data.fullName || !data.email || !data.subject || !data.message) {
      showAlert('Lütfen tüm zorunlu alanları doldurunuz.', 'error');
      return;
    }

    try {
      submitBtn.disabled = true;
      submitBtn.innerHTML = '<i class="fa-solid fa-spinner fa-spin"></i> Gönderiliyor...';

      // Anti-Forgery token extraction
      const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
      const token = tokenElement ? tokenElement.value : '';

      const response = await fetch('/Home/SubmitContact', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'RequestVerificationToken': token
        },
        body: JSON.stringify(data)
      });

      const result = await response.json();

      if (result.success) {
        showAlert(result.message, 'success');
        form.reset();
      } else {
        showAlert(result.message || 'Bir hata oluştu. Lütfen tekrar deneyin.', 'error');
      }
    } catch (err) {
      console.error('Contact Form Submit Error:', err);
      showAlert('Sunucuya bağlanırken bir hata oluştu.', 'error');
    } finally {
      submitBtn.disabled = false;
      submitBtn.innerHTML = originalText;
    }
  });

  function showAlert(message, type) {
    if (!alertToast) return;
    alertToast.className = `alert-toast ${type === 'success' ? 'alert-success-custom' : 'alert-error-custom'}`;
    alertToast.innerHTML = `<i class="${type === 'success' ? 'fa-solid fa-circle-check' : 'fa-solid fa-triangle-exclamation'}"></i> ${message}`;
    alertToast.style.display = 'block';

    setTimeout(() => {
      alertToast.style.display = 'none';
    }, 6000);
  }
}

// Smooth Scroll & Active Nav Tracking
function initSmoothScroll() {
  const navLinks = document.querySelectorAll('.nav-link-item');
  const sections = document.querySelectorAll('section[id]');

  window.addEventListener('scroll', () => {
    let scrollY = window.pageYOffset;

    sections.forEach(current => {
      const sectionHeight = current.offsetHeight;
      const sectionTop = current.offsetTop - 100;
      const sectionId = current.getAttribute('id');

      if (scrollY > sectionTop && scrollY <= sectionTop + sectionHeight) {
        navLinks.forEach(link => {
          link.classList.remove('active');
          if (link.getAttribute('href') === `#${sectionId}`) {
            link.classList.add('active');
          }
        });
      }
    });
  });
}

// Skill Progress Bar Animation on Scroll
function initSkillAnimations() {
  const skillSection = document.getElementById('skills');
  if (!skillSection) return;

  const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        const fills = document.querySelectorAll('.skill-progress-fill');
        fills.forEach(fill => {
          const targetWidth = fill.getAttribute('data-target-width');
          if (targetWidth) {
            fill.style.width = targetWidth;
          }
        });
        observer.unobserve(entry.target);
      }
    });
  }, { threshold: 0.2 });

  observer.observe(skillSection);
}
