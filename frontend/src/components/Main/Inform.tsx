import styles from './Inform.module.css';
import Rectangle from '../../assets/Rectangle .png';
import Container from '../Container/Container.tsx';
import PC from '../../assets/PC.png';
import Btn from '../PCBtn/PCBtn.tsx';

function Inform() {
  return (
    <section className={styles.basis}>
      <Container className={styles.container}>
        <img src={Rectangle} alt={'Rectangle'} className={styles.Rectangle} />
        <img src={PC} alt={'PC'} className={styles.PC} />
        <div className={styles.mainText}>
          <h1 className={styles.box}> Компьютерная коробка </h1>
          <p className={styles.paragraph}>
            Вместе с нами вы сможете проверить совместимость комплектующих для
            вашего PC-зверя или выбрать уже готовую сборку, которую уже за вас
            собрали другие пользователи и проверили временем, именно поэтому
            скорее переходи к сборке возможно своего первого компьютера мечты
          </p>
          <span className={styles.quote}>
            «Соберите PC своей мечты вместе с нами»
          </span>
          <div className={styles.button}>
            <Btn>собрать пк</Btn>
          </div>
        </div>
      </Container>
    </section>
  );
}

export default Inform;
