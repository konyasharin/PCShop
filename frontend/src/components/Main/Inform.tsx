import styles from './Inform.module.css';
import Restangle from '../../assets/Rectangle .png';
import Container from '../Container/Container.tsx';
import PC from '../../assets/PC.png';
function Inform() {
  return (
    <body className={styles.body}>
      <Container className={styles.container}>
        <img src={Restangle} alt={'Restangle'} className={styles.Restangle} />
        <img src={PC} alt={'PC'} className={styles.PC} />
        <div className={styles.mainText}>
          <h1 className={styles.box}> Компьютерная коробка </h1>
          <p className={styles.paragraph}>
            Вместе с нами вы сможете проверить совместимость комплектующих для
            вашего <br /> PC-зверя или выбрать уже готовую сборку, которую уже
            за вас собрали другие пользователи и проверили временем, <br />{' '}
            именно поэтому скорее переходи к сборке возможно своего первого
            компьютера мечты
          </p>
          <q className={styles.quote}>
            «Соберите PC своей мечты вместе с нами»
          </q>
        </div>
      </Container>
    </body>
  );
}

export default Inform;
