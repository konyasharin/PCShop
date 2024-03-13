import styles from './Footer.module.css';
import Container from '../Container/Container.tsx';
import logo from '../../assets/logo.png';
import { NavLink } from 'react-router-dom';
import Vk from '../../assets/Vk.png';
import Tg from '../../assets/Tg.png';
function Footer() {
  return (
    <footer className={styles.footer}>
      <Container className={styles.container}>
        <div className={styles.ellipsis}></div>
        <div className={styles.block}>
          <div className={styles.pen}>
            <img src={logo} alt={'logo'} className={styles.logo} />
          </div>
          <div>
            <NavLink to={'/'} className={styles.link}>
              Главная
            </NavLink>
            <NavLink to={'/PCBuild'} className={styles.link}>
              Новая сборка
            </NavLink>
            <NavLink to={'/builds'} className={styles.link}>
              Сборки
            </NavLink>
            <NavLink to={'/components'} className={styles.link}>
              Комплектующие
            </NavLink>
          </div>
          <div className={styles.social}>
            <a>
              <div className={styles.round}>
                <img src={Vk} alt={'Vk'} />
              </div>
            </a>
            <a>
              <div className={styles.round}>
                <img src={Tg} alt={'Tg'} />
              </div>
            </a>
          </div>
        </div>
      </Container>
    </footer>
  );
}

export default Footer;
