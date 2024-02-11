package com.example.pc.Models;

import lombok.Data;

import javax.persistence.*;

@Entity
@Table(name="Cooler")
@Data
public class CoolerModel {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @Column(name = "brand")
    private String brand;

    @Column(name="model")
    private String model;

    @Column(name="price")
    private Integer price;

    @Column(name="country")
    private String country;

    @Column(name = "speed")
    private Integer speed;

    @Column(name = "power")
    private Integer power;
}
