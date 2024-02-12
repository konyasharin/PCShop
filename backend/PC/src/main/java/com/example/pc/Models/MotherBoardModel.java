package com.example.pc.Models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name="MotherBoard")
@Data
@NoArgsConstructor
@AllArgsConstructor
public class MotherBoardModel {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "brand")
    private String brand;

    @Column(name="model")
    private String model;

    @Column(name="price")
    private Integer price;

    @Column(name="country")
    private String country;

    @Column(name = "frequency")
    private Integer frequency;

    @Column(name="socket")
    private String socket;

    @Column(name="chipset")
    private String chipset;

    @OneToMany
    @JoinColumn(name = "image_id")
    private List<ImageModel> images;

    @Column(name = "preview")
    private byte[] preview;
}
