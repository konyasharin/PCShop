package com.example.pc.Models;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name="PowerUnit")
@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class PowerUnitModel {

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

    @Column(name="battery")
    private String battery;

    @Column(name="voltage")
    private Integer voltage;

    @Column(name = "image")
    @Lob
    private byte[] image;
}
