package com.example.pc.Models;

import lombok.Data;

import javax.persistence.*;

@Entity
@Table(name="PowerUnit")
@Data
public class PowerUnitModel {

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

    @Column(name="battery")
    private String battery;

    @Column(name="voltage")
    private Integer voltage;
}
