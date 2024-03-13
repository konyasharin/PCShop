import ChooseBinCard from 'components/cards/ChooseBinCard/ChooseBinCard.tsx';
import VideoCard from 'assets/videocard.jpg';
import styles from './TrashBin.module.css';
import Btn from 'components/btns/Btn/Btn.tsx';

function TrashBin() {
  return (
    <div className={styles.block}>
      <ChooseBinCard
        className={styles.card}
        name={'Чурка'}
        img={VideoCard}
        text={
          '8GB, здесь типо какие-то характеристики здесь типо какие-то характеристики, здесь типо какие-то харак'
        }
        price={100}
      ></ChooseBinCard>
      <ChooseBinCard
        className={styles.card}
        name={'Чурка'}
        img={VideoCard}
        text={
          '8GB, здесь типо какие-то характеристики здесь типо какие-то характеристики, здесь типо какие-то харак'
        }
        price={100}
      ></ChooseBinCard>
      <div className={styles.end}>
        <h4>Всего:</h4>
        <h2 className={styles.price}>100$</h2>
      </div>
      <Btn className={styles.button}>Оплатить</Btn>
    </div>
  );
}

export default TrashBin;
