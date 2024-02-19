package com.example.pc.Models;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Entity
@Table(name="MotherBoard")
@Data
@Builder
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

    @Column(name="description")
    private String description;

    @Column(name="country")
    private String country;

    @Column(name = "frequency")
    private Integer frequency;

    @Column(name="socket")
    private String socket;

    @Column(name="chipset")
    private String chipset;

    @Column(name = "image")
    @Lob
    private byte[] image;
}
