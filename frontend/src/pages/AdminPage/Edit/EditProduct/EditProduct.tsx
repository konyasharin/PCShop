import UseRadios from 'hooks/useRadios.ts';
import Radio from 'components/Radio/Radio.tsx';
import styles from './EditProduct.module.css';

function EditProduct() {
  const { radios, setRadioIsActive } = UseRadios([
    { text: 'Видеокарты', isActive: false },
    { text: 'Процессоры', isActive: false },
  ]);
  const categoriesBlocks = radios.map((radio, i) => {
    return (
      <Radio
        text={radio.text}
        isActive={radio.isActive}
        onChange={() => setRadioIsActive(i)}
        className={styles.checkBox}
      />
    );
  });
  return (
    <div>
      <h5 className={styles.title}>Категория</h5>
      <div className={styles.checkBoxes}>{...categoriesBlocks}</div>
    </div>
  );
}

export default EditProduct;
