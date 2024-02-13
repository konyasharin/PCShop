package com.example.pc.Models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Entity
@AllArgsConstructor
@NoArgsConstructor
@Data
@Table(name="Images")
public class ImageModel {

    @Id
    @Column(name="image_id")
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long image_id;

    @Lob
    @Column(name="image_data")
    private byte[] image_data;

    @ManyToOne(cascade = CascadeType.REFRESH)
    private ComputerCaseModel computerCaseModel;

    @ManyToOne(cascade = CascadeType.REFRESH)
    private CoolerModel coolerModel;

    @ManyToOne(cascade = CascadeType.REFRESH)
    private MotherBoardModel motherBoardModel;

    @ManyToOne(cascade = CascadeType.REFRESH)
    private PowerUnitModel powerUnitModel;

    @ManyToOne(cascade = CascadeType.REFRESH)
    private ProcessorModel processorModel;

    @ManyToOne(cascade = CascadeType.REFRESH)
    private RAMModel ramModel;

    @ManyToOne(cascade = CascadeType.REFRESH)
    private SSDModel ssdModel;

    @ManyToOne(cascade = CascadeType.REFRESH)
    private VideoCardModel videoCardModel;

}
