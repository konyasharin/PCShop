package com.example.pc.Models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name="Cooler")
@Data
@NoArgsConstructor
@AllArgsConstructor
public class CoolerModel {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "brand")
    private String brand;

    @Column(name="model")
    private String model;

    @Column(name="price")
    private Integer price;

    @Column(name = "description")
    private String description;

    @Column(name="country")
    private String country;

    @Column(name = "speed")
    private Integer speed;

    @Column(name = "power")
    private Integer power;

    @Column(name = "image")
    @Lob
    private byte[] image;
}
